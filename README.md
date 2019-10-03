# Rubik'sCube




##

1. 

2. 

3. 

4.


##Summary:
Magic cube (Rubic’s cube) puzzle game. The player is able to choose the dimensions of the cube and try to solve the puzzle. The player can quit the app and later continue his last game.

Details:
When the application is started the player arrives at the title screen. From the title screen he is able to start a new game or continue his last game if he hasn’t finished it yet.

When starting a new game, the player is able to choose the size of the cube, any size from 2x2x2 to 6x6x6 should be possible. 

Once the player has decided to start a new game or continue his last game, he is transported to the game screen.

On the game screen he can see the cube, a menu button, a timer and an undo button.

Gameplay:
When starting a new game, at first the cube is shown in its solved state and then it starts getting scrambled by rotation. After few seconds the scrambling stops, and from that moment the timer starts counting the time passed, the player is allowed to manipulate the cube and the game is considered to be started. 

After it is started, the game finishes when each face of the cube only contains the same type of tiles. 

At any moment unless specified otherwise, the player is allowed to rotate the camera around the cube. This happens by pressing the screen at any point away from the cube and dragging in a desired direction. The amount of rotation is proportional to the dragged distance, and the axis of rotation is orthogonal to the chosen direction.

At the same time, the player is also allowed to move the camera closer or further from cube within set limits. This happens by pinching the screen or with mouse scroll on PC.

During gameplay, after the game is started and until it is finished, the player is allowed to manipulate the cube. This happens by pressing over one of the cube tiles and dragging
along the cube surface in the desired direction of rotation. Based on that the most suitable axis
of rotation is selected and the related parts of the cube are rotated 90 degrees around it.

All cube manipulations done by the player can be reverted one by one, by pressing the Undo button.

The player can use the menu button to open a dialog that allows him to show or hide the timer, restart the game or go back to the title screen.

If the player chooses to restart the game or go back to the title screen he is first presented with a confirmation dialog. The action is only executed if the player confirms his intention.

When the game is finished, the timer stops counting, the undo button is disabled and a camera animation begins, moving the camera to a set distance from the cube and making a full circle rotating around the cube a few times. When the animation finishes, the player is presented with the ending dialog. 

During the camera animation, the player is not allowed to move the camera.

The ending dialog shows a congratulations message, the time it took for the player to solve the puzzle and options to play again or return to the title screen. The dialog also has a close button, which just closes the dialog and lets the player stay on the game screen.
