## Tasks

|Issue                  | Cause                   | Action                |
|-----------------------|-------------------------|-----------------------|
| Vibrant movement | Mediapip does not capture pose continuously| <ul><li> Adjust the pose detection confidence</li><li>Implement pre-processing of frames</li><li>Interpolate movement data to smooth out transition|
| Floating Model   | Foot positions are not precise| <ul><li> Enforce constraints to ensure feet remain grounded in standing poses</li><li>Or removing ankle control to simplify food positioning|
| Depth | Z-axis calculated by Mediapip is not precise | Utilize an RGB-D camera and fuse depth data with landmarks|
