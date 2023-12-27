# Operation Mode

Operation mode defines what the robot is allowed to do. There are three
options:

- NotReady: The robot is not ready and is not allowed to move in any way
- Automatic: The robot accepts commands from the process control
- Mdi: The robot can do commands over the MDI interface

It is planned to bring the operation mode as tri-state switch to the
hardware. For this the two GPIOs 22 and 27 of the MotoPi board will be
used:

| GPIO 22 | GPIO 27 | Operation Mode             |
|---------|---------|----------------------------| 
| Low     | Low     | Not Ready                  |
| High    | Low     | Automatic                  |
| Low     | High    | MDI                        |
| High    | High    | Illegal state => Exception |
