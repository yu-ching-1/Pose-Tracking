## Used Tools
- Python v3.11 with pip v24
- [Mediapipe](https://developers.google.com/mediapipe/solutions/vision/pose_landmarker)
- IDE VS Code 

## Installation Steps

### Create a new Conda
Open the Command Pelette  <kbd>Ctrl</kbd> + <kbd>Alt</kbd> + <kbd>P</kbd>

Search for **Python: Create Environment**

Choose enviroment type **Conda**

Use **Python 3.11** for this project

### Selete the created Conda as Python interpreter
Open the Command Pelette  <kbd>Ctrl</kbd> + <kbd>Alt</kbd> + <kbd>P</kbd>

Search for **Python: Select Interpreter**

Choose the Conda we created in previous step 
### Install Mediapip
```
$ py -m pip install mediapipe
```




## Refrence Commands
- Export conda environment
```
conda env export > environment.yml
 ```
- Build conda environment from .yml file
 ```
 conda env create -f environment.yml
```




