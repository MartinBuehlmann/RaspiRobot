# RaspiRobot
Implements the RobotControl with EROWA Robot OpenAPI for a Raspberry PI based 
6-axis robot Joy-It Grab-It.

This project is only meant to try out concepts and technologies. But doing this 
with something that moves parts is simply more fun than just doing a plain 
software-only dummy solution.

## EROWA Robot OpenAPI
This is a gRPC based interface that robots must support to be used by EROWA 
process control systems. On the other hand, if a 3rd party process control 
implements the client side of EROWA Robot OpenAPI, it can use EROWA robots to 
automate a production.

### EROWA
EROWA is a Swiss company that helps their customers to automate manufacturing of 
mechanical parts. With the Flexible Manufacturing Concept (FMC), customers can
introduce more safety into the production by standardizing manufacturing from
single machines without any automation up to 7/24 automated production using cells
with a linear robot and different manufacturing machines for milling, grinding, 
lathe or eroding and/or measuring machines.

If you are interested in this topic, please visit at https://www.erowa.com

## Joy-It Grab-It 6-axis robot
This robot can be controlled by a Raspberry PI or by Arduino. For this project a 
Raspberry PI is used. The robot has 6 servos (no step motors). 
Positioning the drives is not so precise, but enough for such educational program. 

![RaspiRobot](/Documents/Images/RaspiRobot_transparent.png)
