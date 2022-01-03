# Walk Plugin

This unofficial TaleSpire allows assets to trigger their walk animation (if they have one) when the asset is moved
using the arrow keys or when using the mouse. (Video: https://youtu.be/1xO52uaanJg)

This plugin, like all others, is free but if you want to donate, use: http://198.91.243.185/TalespireDonate/Donate.php

## Change Log

```
1.0.0: Initial release
```

## Install

Use R2ModMan or similar installer to install this plugin.

## Usage

Your assets will need to implement a pose/animation called "Idle" and "Walk".

Select the asset and then use either the arrow keys or the mouse to move the asset. When the move action is started
the Walk animation will be triggered. When the move action stops, shortly afterwards, the Walk animation will be stopped
and the Idle animation will be triggered.

### Demo Assets

Under "Human" you will find an "Erika" (full body icon). She has both the Idle and Walk animations defined.
