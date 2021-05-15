# CmTetris [![.NET Framework](https://github.com/CSharperMantle/CmTetris/actions/workflows/dotnet-framework.yml/badge.svg?branch=main)](https://github.com/CSharperMantle/CmTetris/actions/workflows/dotnet-framework.yml)

CSharperMantle's Tetris Collection: a series of Tetris-related projects written by CSharperMantle

More information can be found in the [Wiki](https://github.com/CSharperMantle/CmTetris/wiki)

## Contents

### `SimpleTetris`
Simple Tetris game. Built with MVVM designing patterns and requires no other packages other than .NET Framework.

#### Controls
* `A`: Move left
* `D`: Move right
* `S`: Move down
* `Space`: Rotate
* `Esc`: Pause

### `Tetriminify`
A tool to generate tetromino tiling patterns and tetrimino falling orders for arbitary given templates.

![GUI of Tetriminify 1.1.0.0](https://user-images.githubusercontent.com/32665105/108998874-7dc6df00-76dc-11eb-88d0-78ec5dee8abf.png)

*Figure: GUI of Tetriminify 1.1.0.0 with a generated tiling pattern (lower right) and falling orders (lower left) on display. A: Empty block. U: Unavailable block. L, C, I: L-shaped tetrimino, cubic tetrimino, linear tetrimino*

### `Periotris`
A Tetris game based on the theme of the Period Table of Elements.

![Screenshot of Periotris](https://user-images.githubusercontent.com/32665105/108997960-37bd4b80-76db-11eb-8554-237beb8d5d3e.png)

*Figure: In-game screenshot of Periotris 1.5.2.1 with 'Colors' and 'Assistance grid' options enabled*

#### Features
* Purely original and accurate periodic table of elements
* Puzzles are algorithm-guaranteed solvable
* New map each time
* Leaderboards
* Colorful and colorless mode switchable
* Switchable assist grid

#### Controls
* `A` || `Left arrow`: Move left
* `D` || `Right arrow`: Move right
* `S` || `Down arrow`: Move down
* `W` || `Up arrow`: Rotate
* `Space`: Instant drop to the lowest
* `Esc`: Pause

## License

<a rel="license" href="http://www.gnu.org/licenses/gpl-3.0.html"><img alt="GNU GPLv3" src="http://www.gnu.org/graphics/gplv3-with-text-136x68.png"></a>
```plain-text
Copyright (C) 2021 Rong "Mantle" Bao (CSharperMantle)

This program is free software: you can redistribute it and/or modify it under
the terms of the GNU General Public License as published by the Free Software
Foundation, either version 3 of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful, but WITHOUT
ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS
FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with
this program.  If not, see https://github.com/CSharperMantle/CmTetris/blob/main/LICENSE .
```
## Dependencies

### `dotnet/source-build`: A repository to track efforts to produce a source tarball of the .NET Core SDK and all its components
```plain-text
The MIT License (MIT)

Copyright (c) .NET Foundation and Contributors

All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

### `MahApps/MahApps.Metro`: A framework that allows developers to cobble together a better UI
```plain-text
MIT License

Copyright (c) .NET Foundation and Contributors. All rights reserved.

Permission is hereby granted, free of charge, to any person obtaining a copy
of this software and associated documentation files (the "Software"), to deal
in the Software without restriction, including without limitation the rights
to use, copy, modify, merge, publish, distribute, sublicense, and/or sell
copies of the Software, and to permit persons to whom the Software is
furnished to do so, subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY,
FITNESS FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE
AUTHORS OR COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER
LIABILITY, WHETHER IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM,
OUT OF OR IN CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE
SOFTWARE.
```

### `JamesNK/Newtonsoft.Json`: Json.NET is a popular high-performance JSON framework for .NET
```plain-text
The MIT License (MIT)

Copyright (c) 2007 James Newton-King

Permission is hereby granted, free of charge, to any person obtaining a copy of
this software and associated documentation files (the "Software"), to deal in
the Software without restriction, including without limitation the rights to
use, copy, modify, merge, publish, distribute, sublicense, and/or sell copies of
the Software, and to permit persons to whom the Software is furnished to do so,
subject to the following conditions:

The above copyright notice and this permission notice shall be included in all
copies or substantial portions of the Software.

THE SOFTWARE IS PROVIDED "AS IS", WITHOUT WARRANTY OF ANY KIND, EXPRESS OR
IMPLIED, INCLUDING BUT NOT LIMITED TO THE WARRANTIES OF MERCHANTABILITY, FITNESS
FOR A PARTICULAR PURPOSE AND NONINFRINGEMENT. IN NO EVENT SHALL THE AUTHORS OR
COPYRIGHT HOLDERS BE LIABLE FOR ANY CLAIM, DAMAGES OR OTHER LIABILITY, WHETHER
IN AN ACTION OF CONTRACT, TORT OR OTHERWISE, ARISING FROM, OUT OF OR IN
CONNECTION WITH THE SOFTWARE OR THE USE OR OTHER DEALINGS IN THE SOFTWARE.
```