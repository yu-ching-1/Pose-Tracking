import mediapipe as mp
import numpy as np
import cv2
import time
import threading
import configuration as cfg
import global_vars as gv

from mediapipe.framework.formats import landmark_pb2
from mediapipe import solutions

class CaptureThread(threading.Thread):
    cap = None
    ret = None
    frame = None
    timestamp = None
    isRunning = False

    def run(self) -> None:
        if cfg.DEBUG_MODE: print('opening cameera...')
        self.cap = cv2.VideoCapture(cfg.CAM_INDEX,cv2.CAP_DSHOW)
        self.cap.set(cv2.CAP_PROP_FRAME_HEIGHT,cfg.CAM_HEIGHT)
        self.cap.set(cv2.CAP_PROP_FRAME_WIDTH,cfg.CAM_WIDTH)
        self.cap.set(cv2.CAP_PROP_FPS,cfg.CAM_FPS)
       
        time.sleep(1)

        while gv.RUNNING:
            self.ret, self.frame = self.cap.read()
            self.isRunning = True
           

        return super().run()

class DetectionThread(threading.Thread):
    
    vision =  mp.tasks.vision
    def set_landmarks_style(self,_thickness:int, _radius:int, left_color:tuple[int,int,int],right_color:tuple[int, int, int]):
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

    
    def draw_landmark_on_image(self,img:mp.Image,result:vision.PoseLandmarkerResult):
        
        pose_landmark_list = result.pose_landmarks
        annotated_img = np.copy(img)
        pose_landmarks_proto = landmark_pb2.NormalizedLandmarkList()
        
        for pose_landmarks in pose_landmark_list:
              
            pose_landmarks_proto.landmark.extend([landmark_pb2.NormalizedLandmark(x=landmark.x, y=landmark.y, z=landmark.z) for landmark in pose_landmarks
                                                ])
            
            if cfg.DISPLAY_STREAM:
                
                style = self.set_landmarks_style(cfg.LMKER_THICKNESS,cfg.LMKER_RADIUS,cfg.LMKER_LEFT,cfg.LMKER_RIGHT)
                solutions.drawing_utils.draw_landmarks(
                annotated_img,
                pose_landmarks_proto,
                solutions.pose.POSE_CONNECTIONS,
                style)
                # annotated_img =cv2.cvtColor(annotated_img,cv2.COLOR_RGB2BGR)
        
        return annotated_img,pose_landmarks_proto
    
    def run(self) -> None:
        options = self.vision.PoseLandmarkerOptions(
        base_options = mp.tasks.BaseOptions(model_asset_path = cfg.MODEL_PATH()),
        running_mode = self.vision.RunningMode.IMAGE
    )
        capture = CaptureThread()
        capture.start()
    
        with self.vision.PoseLandmarker.create_from_options(options) as landmarker:
            
            print('Waiting for camera...')
            while gv.RUNNING and not capture.isRunning:
                   
                   time.sleep(0.5)
            
            print('Camera opened')

            while gv.RUNNING and capture.cap.isOpened():
                img = capture.frame
                mp_img = mp.Image(image_format=mp.ImageFormat.SRGB,data = img)
                results = landmarker.detect(mp_img)
                annotated_img, n_result = self.draw_landmark_on_image(mp_img.numpy_view(),results)
                
                if cfg.DEBUG_MODE: print(n_result)
                if cfg.DISPLAY_STREAM:
                    cv2.imshow('Pose Detection',annotated_img)
                    cv2.waitKey(5)


        return super().run()

