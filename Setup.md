<style>
    keyword
    {
        color:green;
        background:lightgreen;
        font-style:italic
    }
</style>
## Used Tools
- [Python v3.11.8](https://www.python.org/downloads/release/python-3118/) with pip v24
- [Mediapipe](https://developers.google.com/mediapipe/solutions/vision/pose_landmarker)
- IDE [VS Code](https://code.visualstudio.com/Download) v1.87
- opencv-python v4.9(download via pip)

## Installation Steps

### Create a New Conda
Open the Command Palette  <kbd>Ctrl</kbd> + <kbd>Alt</kbd> + <kbd>P</kbd>

Search for <keyword>Python: Create Environment</keyword>

Choose environment type <keyword>Conda</keyword>

Use <keyword>Python 3.11.8</keyword> for this project

### Select the Created Conda as Python Interpreter
Open the Command Palette  <kbd>Ctrl</kbd> + <kbd>Alt</kbd> + <kbd>P</kbd>

Search for <keyword>Python: Select Interpreter</keyword>

Choose the Conda we created in previous step 
### Install Mediapip
```
$ py -m pip install mediapipe
```
For using the pose detection package, it requires you to download the models and store in a local path.
### Install Opencv-python
```
$ py -m pip install opencv-python
```
### Install Unity
Download and install [Unity Hub](https://unity.com/download)

Open Unity Hub

 - Install Editor v2022.3.21f1
 - Add a new project
 - Open **Window** -> **Package** and make sure the version of <keyword>visual studio editor</keyword> is above 2.0.20
- Open **Edit** -> **Preference** and set the <keyword>External Script Editor</keyword> in External Tool as <keyword>Visual Studio Code</keyword> 

## Unity Tutor
- Source: This [video](https://youtu.be/RQ-2JWzNc6k?si=Nk6mlM9eXPqGsdWP) is very helpful for beginner of Unity. It used object  **Line** between landmarks which I changed it to **Cylinder** for better practice of rigid body transform calculation.  


## Reference Commands
- Export conda environment
```
conda env export > environment.yml
 ```
- Build conda environment from .yml file
 ```
 conda env create -f environment.yml
```




