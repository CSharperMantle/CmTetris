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
A tool to generate closely-packed Tetrimino patterns and falling orders for arbitary given templates.

**Notice:** This tool is fragile. Use it properly ;)

### `Periotris`
A Tetris game based on the theme of the Period Table of Elements.

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
Copyright (C) 2021 Rong "Mantle" Bao (CSharperMantle)

This program is free software: you can redistribute it and/or modify it under the terms of the GNU General Public License as published by the Free Software Foundation, either version 3 of the License, or (at your option) any later version.

This program is distributed in the hope that it will be useful, but WITHOUT ANY WARRANTY; without even the implied warranty of MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU General Public License for more details.

You should have received a copy of the GNU General Public License along with this program.  If not, see https://github.com/CSharperMantle/CmTetris/blob/main/LICENSE .

## Dependencies
### `mathnet/mathnet-numerics`: Math.NET Numerics
```plain-text
Copyright (c) 2002-2021 Math.NET

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

### `kazuyaujihara/NCDK`: The Chemistry Development Kit ported to .NET
```plain-text
Copyright (c) 2016-2021 Kazuya Ujihara

This library is free software; you can redistribute it and/or
modify it under the terms of the GNU Lesser General Public
License as published by the Free Software Foundation; either
version 2.1 of the License, or (at your option) any later version.

This library is distributed in the hope that it will be useful,
but WITHOUT ANY WARRANTY; without even the implied warranty of
MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
Lesser General Public License for more details.

You should have received a copy of the GNU Lesser General Public
License along with this library; if not, write to the Free Software
Foundation, Inc., 51 Franklin Street, Fifth Floor, Boston, MA  02110-1301
USA
```

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
