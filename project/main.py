import global_vars

import landmarks_detection

mainthread = landmarks_detection.DetectionThread()
mainthread.start()

print('press any key to stopp')
i = input()
global_vars.RUNNING = False

exit()