1.) Git Hub url = **https://github.com/Sohit1991/K8sAssignmt**

**2.) Docker hub image url** = **https://hub.docker.com/r/sohit1991/orderapi (sohit1991/orderapi:latest)**

3.) Docker hub Db Image url = **https://hub.docker.com/_/microsoft-mssql-server (mcr.microsoft.com/mssql/server:2017)**

4.) API Access Url= **http://35.192.162.173/swagger/index.html**

5.) Video Url = **https://drive.google.com/file/d/1BRhjfjClcdLO86moBZtKYbCkUoth74d3/view?usp=drive_link
**
6.) For **HPA** testing , I used **Load-Generator** using below command = kubectl run -i --tty load-generator --rm --image=busybox:1.28 --restart=Never -- /bin/sh -c "while sleep 0.01; do wget -q -O- http://35.192.162.173/api/v1/productOrder; done"

6.) YAML Files in folder "YAML" in solution structure and Docker file is also in code repository 


7.) During the video, copied some text from Notepad like for load-generator and POST API request

