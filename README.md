## Unity Depth Camera

Provides a quick and easy way to map [depth texture](https://docs.unity3d.com/Manual/SL-CameraDepthTexture.html) values to RGB channels. I wrote it for feeding depth info to [Unity Machine Learning Agents](https://github.com/Unity-Technologies/ml-agents) as visual observations.

You can define distance ranges by setting min and max values. Clamped distances are being mapped to the full 8-bit color range. Default depth values are flipped so that close objects are more saturated than farther-away ones.

### Example settings

<img src="images/dc1.png" align="middle" width="1407"/>

Red channel only.<br><br>

<img src="images/dc2.png" align="middle" width="1407"/>

Green channel only with inverted depth values.<br><br>

<img src="images/dc4.png" align="middle" width="1407"/>

One color channel per distance range, increases the total depth resolution compared to using a single channel.<br><br>

<img src="images/dc3.png" align="middle" width="1407"/>

Blue channel limited to foreground, mixed with source image's red and green channels.<br><br>

<img src="images/dc5.png" align="middle" width="1407"/>

All channels limited to foreground, inverted.<br><br>

