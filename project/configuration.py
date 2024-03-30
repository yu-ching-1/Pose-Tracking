from global_vars import Pose_Models


# Pose model
POSE_MODEL = Pose_Models.HEAVY

MODEL_PATH_LITE = ''
MODEL_PATH_FULL = ''
MODEL_PATH_HEAVY = 'c:/Users/YuChi/Documents/GitHub/Pose-Tracking/project/model/pose_landmarker_heavy.task'

# Camera Setting
CAM_INDEX = 0
CAM_WIDTH = 480
CAM_HEIGHT = 480
CAM_FPS = 60

# Outputs
DEBUG_MODE = False
DISPLAY_STREAM = True

#Communication with Unity
BROADCAST = True
UDP_ADDRESS = '127.0.0.1' #local host
UDP_PORT = 5024

# Landmark Style
LMK_THICKNESS = 2
LMK_RADIUS = 5
LMK_LEFT = (0,0,0)
LMK_RIGHT = (200,0,0)



def MODEL_PATH():
    switch = {
        Pose_Models.LITE: MODEL_PATH_LITE,
        Pose_Models.FULL: MODEL_PATH_FULL,
        Pose_Models.HEAVY: MODEL_PATH_HEAVY,
    }
    return switch.get(POSE_MODEL)