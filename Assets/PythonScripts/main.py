import cv2
from cvzone.HandTrackingModule import HandDetector
import socket

height = 720
width = 1280

cap = cv2.VideoCapture(0)
cap.set(3, 640)
cap.set(4, 480)

print("set up camera")
sock = socket.socket(socket.AF_INET, socket.SOCK_DGRAM)
serverAddressPort = ("127.0.0.1", 5052)
print("Set up scoket")
detector = HandDetector(maxHands=2, detectionCon=.8)
print("set up detecter")
while True:
    success, img = cap.read()
    # Hands
    hands, img = detector.findHands(img)

    data = []

    if hands:
        hand = hands[0]

        lmList = hand['lmList']
        # print(lmList)
        for lm in lmList:
            data.extend([lm[0], height - lm[1], lm[2]])
        # print(data)
        sock.sendto(str.encode(str(data)), serverAddressPort)

    # img = cv2.resize(img, (0, 0), None, .5, .5)
    cv2.imshow("Image", img)
    cv2.waitKey(1)
