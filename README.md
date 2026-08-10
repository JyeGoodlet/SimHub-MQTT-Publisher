# SimHub-MQTT-Publisher

A fork of [SHWotever/SimHub-MQTT-Publisher](https://github.com/SHWotever/SimHub-MQTT-Publisher).

This version exists because I did not want to rebuild the project every time I wanted to add a new SimHub property.

## Overview

This project enables real-time streaming of racing simulation telemetry from SimHub to an MQTT broker, allowing integration with other systems and dashboards.

## Features

- Publishes SimHub telemetry data to MQTT topics
- Real-time data streaming
- Easy integration with MQTT brokers

## Development

### Prerequisites

- [SimHub](https://www.simhubdash.com/) installed locally
- Add the SimHub install folder to the `SIMHUB_INSTALL_PATH` environment variable
- .NET Framework
- MQTT broker (e.g., Mosquitto, HiveMQ)

Example on Windows:

```powershell
setx SIMHUB_INSTALL_PATH "C:\Program Files (x86)\SimHub"
```

### Build

1. Clone the repository.
2. Open the solution in Visual Studio.
3. Build the project in Release or Debug.
4. Run or debug the plugin from Visual Studio.

## Installation

1. Download the latest release zip from GitHub Releases.
2. Extract the zip.
3. Copy these files into your SimHub folder, for example `C:\Program Files (x86)\SimHub`:
   - `SimHub.MQTTPublisher.dll`
   - `SimHub.MQTTPublisher.pdb`
   - `MQTTnet.dll`
   - `payload_config.json`
4. Restart SimHub.
5. Configure your MQTT settings.

The release zip should contain these files.

## Usage

Configure the MQTT connection parameters and start the application to begin publishing SimHub telemetry data.

### Configure MQTT

Set these values in the plugin settings inside SimHub:

- `Server`: your MQTT broker host name or IP address
- `Topic`: the MQTT topic to publish to
- `Login`: your MQTT username, if required
- `Password`: your MQTT password, if required

The default values are `localhost`, `racing/driver_name`, `admin`, and `admin`.

### Updating `payload_config.json`

`payload_config.json` controls which SimHub properties are included in the MQTT payload.

1. Open SimHub and go to `Available properties`.
2. Search for the property you want to add.
3. Use only the last section of the path, such as `GameData.IsInPit` or `GameData.PitLimiterOn`.
4. Open the `payload_config.json` file in your SimHub folder.
5. Edit the `Fields` array.
6. Add or remove exact SimHub property names from `StatusDataBase`.
7. Keep `time` and `userId` if you want the built-in fields.
8. Save the file.
9. Restart SimHub so the plugin reloads the updated config and MQTT updates continue.

If a field name does not exist in SimHub, it will be skipped.

The screenshot below shows the `Available properties` screen in SimHub. You only need the last property section for the payload config.

If you add the screenshot to the repository, place it under `docs/` and reference it here.

### Home Assistant example

Example automation using telemetry published to the `racing` MQTT topic:

```yaml
alias: SIM FLAGS
description: ""
mode: single
triggers:
  - trigger: mqtt
    topic: racing
conditions:
  - condition: template
    value_template: "{{ is_state('input_boolean.sim_time_toggle', 'off') }}"
actions:
  - variables:
      ignition_on: "{{ trigger.payload_json.EngineIgnitionOn | int(0) == 1 }}"
      pit_limiter_on: "{{ trigger.payload_json.PitLimiterOn | int(0) == 1 }}"
      flag: >
        {% if trigger.payload_json.Flag_Black | int(0) == 1 %}black
        {% elif trigger.payload_json.Flag_Blue | int(0) == 1 %}blue
        {% elif trigger.payload_json.Flag_Checkered | int(0) == 1 %}checkered
        {% elif trigger.payload_json.Flag_Yellow | int(0) == 1 %}yellow
        {% elif trigger.payload_json.Flag_Green | int(0) == 1 %}green
        {% elif trigger.payload_json.Flag_White | int(0) == 1 %}white
        {% elif trigger.payload_json.Flag_Orange | int(0) == 1 %}orange
        {% else %}none
        {% endif %}

  - choose:
      # Ignition OFF -> neutral/reset
      - conditions:
          - condition: template
            value_template: "{{ not ignition_on }}"
        sequence:
          - action: input_boolean.turn_off
            target:
              entity_id: input_boolean.sim_flag_caution_mode
          - action: input_boolean.turn_off
            target:
              entity_id: input_boolean.sim_flag_race_over
          - action: light.turn_on
            target:
              device_id: your_device_id_here
            data:
              rgb_color: [255, 255, 255]
              brightness_pct: 30
              transition: 1

      # Pit limiter override
      - conditions:
          - condition: template
            value_template: "{{ ignition_on and pit_limiter_on }}"
        sequence:
          - action: light.turn_on
            target:
              device_id: your_device_id_here
            data:
              rgb_color: [170, 0, 255]
              brightness_pct: 85
              transition: 1

      - conditions:
          - condition: template
            value_template: "{{ ignition_on and flag == 'yellow' }}"
        sequence:
          - action: input_boolean.turn_on
            target:
              entity_id: input_boolean.sim_flag_caution_mode
            data: {}
          - action: light.turn_on
            target:
              device_id: your_device_id_here
            data:
              rgb_color: [255, 234, 0]
              brightness_pct: 70
              transition: 1

      - conditions:
          - condition: template
            value_template: "{{ ignition_on and flag == 'green' }}"
        sequence:
          - action: input_boolean.turn_off
            target:
              entity_id: input_boolean.sim_flag_caution_mode
          - action: input_boolean.turn_off
            target:
              entity_id: input_boolean.sim_flag_race_over
          - action: light.turn_on
            target:
              device_id: your_device_id_here
            data:
              rgb_color: [0, 255, 0]
              brightness_pct: 80
              transition: 1

      - conditions:
          - condition: template
            value_template: "{{ ignition_on and flag == 'blue' }}"
        sequence:
          - action: light.turn_on
            target:
              device_id: your_device_id_here
            data:
              rgb_color: [0, 80, 255]
              brightness_pct: 75
              transition: 1

      - conditions:
          - condition: template
            value_template: "{{ ignition_on and flag == 'black' }}"
        sequence:
          - action: light.turn_on
            target:
              device_id: your_device_id_here
            data:
              rgb_color: [255, 0, 0]
              brightness_pct: 100
              transition: 1

      - conditions:
          - condition: template
            value_template: "{{ ignition_on and flag == 'white' }}"
        sequence:
          - action: light.turn_on
            target:
              device_id: your_device_id_here
            data:
              rgb_color: [255, 255, 255]
              brightness_pct: 60
              transition: 1

      - conditions:
          - condition: template
            value_template: "{{ ignition_on and flag == 'orange' }}"
        sequence:
          - action: light.turn_on
            target:
              device_id: your_device_id_here
            data:
              rgb_color: [255, 120, 0]
              brightness_pct: 90
              transition: 1

      - conditions:
          - condition: template
            value_template: "{{ ignition_on and flag == 'checkered' }}"
        sequence:
          - action: input_boolean.turn_on
            target:
              entity_id: input_boolean.sim_flag_race_over
          - action: light.turn_on
            target:
              device_id: your_device_id_here
            data:
              rgb_color: [255, 255, 255]
              brightness_pct: 100
              transition: 1

    # No flag active while ignition ON -> neutral/reset
    default:
      - action: input_boolean.turn_off
        target:
          entity_id: input_boolean.sim_flag_caution_mode
      - action: light.turn_on
        target:
          device_id: your_device_id_here
        data:
          rgb_color: [255, 255, 255]
          brightness_pct: 40
          transition: 1
```

## License

[Add your license information here]

## Contributing

Contributions are welcome! Please feel free to submit pull requests or open issues.
