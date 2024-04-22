import socket
import threading

import configuration as cfg
import global_vars as gv

class UdpThread(threading.Thread):
    landmarks = []

    def run(self) -> None:

        sock = socket.socket(socket.AF_INET,socket.SOCK_DGRAM)
        sock_address =(cfg.UDP_ADDRESS,cfg.UDP_PORT) 
        sock.connect(sock_address)

        while cfg.BROADCAST and gv.RUNNING:

            if len(self.landmarks) > 0:
                lmlist=[]
               
                for lm in self.landmarks:
                    lmlist.append(lm.x)
                    lmlist.append(lm.y)
                    lmlist.append(lm.z)
                
                sock.send(str.encode(str(lmlist)))
           
                self.landmarks = []

        return super().run()