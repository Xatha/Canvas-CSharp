# Canvas-CSharp
This library is a C# port of <a href="https://github.com/kfl/diku-canvas">diku-canvas</a> which is developed by <a href="https://github.com/kfl">Ken Friis Larsen</a> for The Department of Computer science (DIKU) at Copenhagen University.
Like the F# implentation, this library is a simple functional graphics library which is an abstraction of SDL2, featuring a number of simple functions for devoloping and running 2d graphics on a canvas. 

# How to use Canvas-CSharp
We start by creating a window. 

```c#
var window = new Window("Name", 500, 500);
```
We supply the window with the window title and resolution.

```c#
Canvas Draw(Canvas canvas)
{
    var width = canvas.ViewWidth;
    var height = canvas.ViewHeight;
    
    Drawer.SetLine(canvas, ColorPicker.Black, new Point(0,0), new Point(width, height));
    return canvas;
}
```
We defined a function that returns a Canvas and takes canvas as argument. ``Drawer.SetLine();`` draws a line on the canvas. We give it a color and the points to draw from and to. We can now run our app.
```c#
window.RunSimpleApp(Draw);
```
``RunSimpleApp()`` will take in our draw function as argument and run it internally. 
![image](https://user-images.githubusercontent.com/43752641/200373477-33f8e1d3-f2f4-4a22-9544-bfe4ed620416.png)

And just like that we have an application!

## The interactive application
But often we want to do more than just show a static image. We want make stuff react based on user input! For this, we have the ``RunApp()`` function.
``RunApp()`` takes a generic type as our "state", a ``draw`` function, and a ``onKeyPressed`` function. 
There is an example posted in examples folder to help you get started! 

# Disclamer
This library has no affiliations with DIKU nor Larsen. I wrote this as an excercise to try to understand and explore SDL2 and <a href="https://github.com/kfl/diku-canvas">diku-canvas</a> internal systems. This port is not 1:1. Some functions like ``Show()`` is missing and will not be ported over, while functions like the ``turtleCmds`` haven't been ported yet.
