## Inventory Robot Control

This project controls a **Universal Robots (UR)** arm and an **OnRobot gripper** through a **C# TCP client**.
It’s part of the *Inventory System Control* assignment, automating pick-and-place operations between bins in a simulated warehouse.

## How It Works

* The **C# program** connects to **URSim** via TCP:

  * Port **29999** → dashboard commands (e.g., brake release).
  * Port **30002** → sends URScript for motion and gripper control.
* The **URScript** runs directly on the robot and:

  * Connects to the **OnRobot gripper** via XML-RPC (`http://localhost:41414`).
  * Defines positions `p1–p11` and moves between them with `movej` and `movel`.
  * Uses `rg_grip(width, force)` to open/close the gripper during each sequence.

## Sequence Overview

1. Start from a safe home pose.
2. Move to pickup position → close gripper.
3. Move to next bin → open gripper.
4. Repeat across several bins to simulate an inventory transfer.
