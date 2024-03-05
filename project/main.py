import numpy as np
import cv2
import mediapipe as mp
from mediapipe.framework.formats import landmark_pb2

from mediapipe import solutions
import cv2

model_path = 'c:/Users/YuChi/Documents/GitHub/Pose-Tracking/project/model/pose_landmarker_heavy.task'
image_path = 'c:/Users/YuChi/Documents/GitHub/Pose-Tracking/running-man.jpg'
Baseoption = mp.tasks.BaseOptions
PoseLandmarker = mp.tasks.vision.PoseLandmarker
PoseLandMarkerOption = mp.tasks.vision.PoseLandmarkerOptions
PoseLandMarkerResult = mp.tasks.vision.PoseLandmarkerResult
VisionRunningMode = mp.tasks.vision.RunningMode

def print_result(result: PoseLandMarkerResult, output_image:mp.Image, timestamp_ms: int ):
    
    print(timestamp_ms)
    annotated_image = draw_landmark_on_image(output_image,result)
    cv2.imshow('test',cv2.cvtColor(annotated_image,cv2.COLOR_RGB2BGR))
    
    return 0

def draw_landmark_on_image(img:mp.Image,result:PoseLandMarkerResult):
    pose_landmark_list = result.pose_landmarks
    annotated_img = np.copy(img)

    for pose_landmarks in pose_landmark_list:
        pose_landmarks_proto = landmark_pb2.NormalizedLandmarkList()
        
        pose_landmarks_proto.landmark.extend([landmark_pb2.NormalizedLandmark(x=landmark.x, y=landmark.y, z=landmark.z) for landmark in pose_landmarks
                                              ])
        
        

        style = set_landmarks_style(2,10,(0,0,0),(0,255,0))
        solutions.drawing_utils.draw_landmarks(
            annotated_img,
            pose_landmarks_proto,
            solutions.pose.POSE_CONNECTIONS,
            style)
        
        print(pose_landmarks_proto.landmark)


    return annotated_img

def set_landmarks_style(_thickness:int, _radius:int, left_color:tuple[int,int,int],right_color:tuple[int, int, int]):
    from mediapipe.python.solutions.drawing_utils import DrawingSpec
    import mediapipe.python.solutions.drawing_styles as dt
    from mediapipe.python.solutions.pose import PoseLandmark
    pose_landmark_style = {}
    left_spec = DrawingSpec(
      color=left_color, thickness=_thickness,circle_radius=_radius)
    right_spec = DrawingSpec(
      color=right_color, thickness=_thickness,circle_radius=_radius)
    for landmark in dt._POSE_LANDMARKS_LEFT:
        pose_landmark_style[landmark] = left_spec
    for landmark in dt._POSE_LANDMARKS_RIGHT:
        pose_landmark_style[landmark] = right_spec
    pose_landmark_style[PoseLandmark.NOSE] = DrawingSpec(
        color=(224, 224, 224), thickness=_thickness)
    return pose_landmark_style

def detect_from_image(options:PoseLandMarkerOption):
    landmarker = PoseLandmarker.create_from_options(options_image)
    mp_image = mp.Image.create_from_file(image_path)
    pose_landmarker_result = landmarker.detect(mp_image)

    annotated_image = draw_landmark_on_image(mp_image.numpy_view(), pose_landmarker_result)
    height = 480
    width = 480
    dim = (height, width)

    annotated_image = cv2.resize(annotated_image,dim)
    cv2.imshow('test',cv2.cvtColor(annotated_image,cv2.COLOR_RGB2BGR))
    cv2.waitKey(0)

    return 0

options_live = PoseLandMarkerOption(
base_options=Baseoption(model_asset_path=model_path),
running_mode = VisionRunningMode.LIVE_STREAM,
result_callback = print_result
)

options_image = PoseLandMarkerOption(
base_options=Baseoption(model_asset_path=model_path),
running_mode = VisionRunningMode.IMAGE
)

detect_from_image(options_image)


# cap = cv2.VideoCapture(0)

# while True:
#     ret, frame = cap.read()
#     timestamp = cap.get(cv2.CAP_PROP_POS_MSEC)
#     mp_image = mp.Image(image_format=mp.ImageFormat.SRGB, data=frame)
#     detection_result = landmarker.detect(mp_image)
    
#     print(timestamp)

#     cv2.imshow('test',frame)
#     if cv2.waitKey(1000) == ord('q'):
#         break

# cap.release()
# cv2.destroyAllWindows()
