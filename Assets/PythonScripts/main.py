import cv2
from cvzone.HandTrackingModule import HandDetector
import socket

height = 720
width = 1280

print("Setting of video camera")
cap = cv2.VideoCapture(1)
cap.set(3, 1280)
cap.set(4, 720)

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

    fullData = []
    data = []

    if hands:
        hand = hands[0]

        lmList = hand['lmList']
        # print(lmList)
        for lm in lmList:
            data.extend([lm[0], height - lm[1], lm[2]])
        # print(data)

        fullData.append(data)

        data2 = []
        if len(hands) > 1:
            hand2 = hands[1]
            lmList2 = hand2['lmList']
            # print(lmList)
            for lm in lmList2:
                data2.extend([lm[0], height - lm[1], lm[2]])
            # print(data)
            fullData.append(data2)

        # print(fullData)
        # print(len(fullData))
        sock.sendto(str.encode(str(fullData)), serverAddressPort)
    else:
        sock.sendto(str.encode("No hand Detected"), serverAddressPort)

    img = cv2.resize(img, (0, 0), None, .5, .5)
    cv2.imshow("Image", img)
    cv2.waitKey(1)
    
