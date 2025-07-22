# WmsGfxSpriteEditor User Guide

## Overview
**WmsGfxSpriteEditor** (short for Williams Graphics Sprite Editor) is a Windows application for viewing and editing sprite graphics in classic arcade game *Robotron: 2084*. It provides basic tools for sprite manipulation, palette editing, and ROM management.

---

## Getting Started

### Installation
1. Download and run the installer (`WmsGfxSpriteEditor.Setup.exe`).
2. Follow the on-screen instructions to complete installation.

## Extract ROMs to be edited to a folder

1. Extract the Robotron ROMs you wish to edit to a folder. Please see Appendices for what the files should look like.

### Launching the Application
- Open **WmsGfxSpriteEditor** from your Start Menu or desktop shortcut.

---

## Loading ROMs
1. Go to `File > Load Robotron ROMs`.
2. Choose the ROM set type:
   - Blue Label
   - Tie Die WDPU
   - Tie Die MAME
3. Browse to the folder containing your ROM files and select it. Refer to the  appendices for a list of the files that need to be present for each version.
4. The editor will audit and load the ROM data. 

---

## Sprite Editing

### Selecting a Sprite
- Use the sprite dropdown at the top to select a sprite to view or edit.

### Editing Tools
- **Draw**: Click on the sprite grid to change pixel colors using the selected palette color.
- **Undo/Redo**: Use the Edit menu or toolbar to undo or redo changes.
- **Copy/Paste**: Copy the current sprite to the clipboard or paste from the clipboard. **NOTE: Only images the same size or smaller, with exactly the same RGB colours, can be pasted from the clipboard.** 
- **Flip/Shift**: Use the Sprite menu to flip or shift the sprite horizontally/vertically.

**NB: You may be much faster copying a sprite to the clipboard, editing it in an app like Paint.NET and pasting it back into the sprite editor app, than using the inbuilt editing tools.**

### Zoom
- Use the `View` menu Zoom In | Zoom Out or the zoom controls in the status bar to zoom in/out the sprite. **NOTE: You can also use CTRL+ the Mouse Wheel for this.**
- The current zoom level is displayed in the status bar.
- The `View` menu includes an **Auto Zoom To Window** option. When enabled, the editor will automatically zoom to fit the sprite whenever you change the active sprite. This ensures you can always see the full sprite. This can be toggled on or off, and is accessible via the shortcut `Ctrl+Shift+B`.

---

## Palette Editing
- Click the palette button or use `View > Palette` to open the palette editor.
- Select a color to use for drawing.
- Right-click a color for options to copy its RGB or Hex value.
- If you are going to use a paint app to edit the sprites before pasting them back in, use Windows' inbuilt Snipping Tool to take a copy of the colour palette. That way you can paste that across too, and use it with the colour dropper.

---

## Saving Changes
- Use `File > Save` to write your changes back to the ROM files. 


-## Running edited ROMs with MAME 

- **Replace** the files in the Robotron ROM zip files with the corresponding edited ones.
- Start MAME and load the game as before. **Ignore** any checksum error warnings.
---

## Clipboard Support
- The editor supports copying and pasting sprites and palette colors using the system clipboard. 

---

## Status Bar
- Displays the current sprite, coordinates, and zoom level.

---

## Help
- For information about the application, go to `Help > About`.

---

## Keyboard Shortcuts

| Action                 | Shortcut         |
|------------------------|------------------|
| Undo                   | Ctrl+Z           |
| Redo                   | Ctrl+Y           |
| Copy Sprite            | Ctrl+C           |
| Paste                  | Ctrl+V           |
| Zoom In                | Ctrl+Plus        |
| Zoom Out               | Ctrl+Minus       |
| Show Palette           | Ctrl+P           |
| Auto Zoom To Window    | Ctrl+Shift+B |

---

## Troubleshooting
- If ROM files are missing or invalid, a dialog will list the missing files.
- Ensure you have the correct ROM set for your selected game/version.

---

## Feedback & Support
For questions or feedback, please contact the developer at scott.tunstall@ntlworld.com.




## APPENDICES

### Required Blue Label ROM files

<img width="617" height="454" alt="image" src="https://github.com/user-attachments/assets/983d7b6c-19dc-41e2-8794-816512059ab6" />

### Required Williams Defender Players Unite (WDPU) TIE-die ROM files

<img width="648" height="513" alt="image" src="https://github.com/user-attachments/assets/9b8e16be-2f00-4d64-81b2-92d87f333dd8" />

### Required Tie-die ROM (MAME) files

<img width="618" height="455" alt="image" src="https://github.com/user-attachments/assets/6e15538b-613d-4409-9d57-7abfdb028a19" />









