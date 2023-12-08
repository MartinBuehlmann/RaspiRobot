# RaspiRobot
Implements the RobotControl with EROWA Robot OpenAPI for a Raspberry PI based 6-axis robot Joy-It Grab-It.

##EROWA Robot OpenAPI
This is a gRPC based interface that robots must support to be used by EROWA process control systems. Respectively if a 3rd party process control implements the client side of EROWA Robot OpenAPI, it can use EROWA robots to automate a production.

##Joy-It Grab-It 6-axis robot
This robot can be controlled by a Raspberry PI or by Arduino. For this project a Raspberry PI is used. The robot has 6 servos (no step motors). Positioning the drives is not so precise, but enough for such educational program. Servos do not support different speeds, and so a slower speed can only be realized by calculating steps between start and end point and wait for some time between the steps.
