# SimHub-MQTT-Publisher

A C# application that publishes SimHub telemetry data over MQTT.

## Overview

This project enables real-time streaming of racing simulation telemetry from SimHub to an MQTT broker, allowing integration with other systems and dashboards.

## Features

- Publishes SimHub telemetry data to MQTT topics
- Real-time data streaming
- Easy integration with MQTT brokers

## Getting Started

### Prerequisites

- [SimHub](https://www.simhubdash.com/)
- .NET Framework
- MQTT broker (e.g., Mosquitto, HiveMQ)

### Installation

1. Clone the repository
2. Build the project in Visual Studio
3. Configure your MQTT broker connection settings
4. Run the application

## Usage

Configure the MQTT connection parameters and start the application to begin publishing SimHub telemetry data.

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
