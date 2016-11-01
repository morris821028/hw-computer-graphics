## README ##

### 操作說明 ###

* 使用 __方向鍵__ 控制持續的旋轉方向與速度
* 使用 __PageUp/PageDown__ 控制物體離攝影機的距離
* 使用 __Home/End__ 控制物體 y-axis
* 使用 __Insert/Delete__ 控制物體 x-axis
* 使用 __滑鼠拖曳__ 旋轉物體
* 使用 __Tab/Alt__ shear 物體
* 使用 __Shift/Ctrl__ scale 物體

### 程式細節 ###

#### 變數修飾 ####

[參考](http://www.lighthouse3d.com/tutorials/glsl-tutorial/data-types-and-variables/)

Qualifiers give a special meaning to the variable. The following qualifiers are available:

* `const` – The declaration is of a compile time constant.
* `attribute` – Global variables that may change per vertex, that are passed from the OpenGL application to vertex shaders. This qualifier can only be used in vertex shaders. For the shader this is a read-only variable. See Attribute section.
* `uniform` – Global variables that may change per primitive [...], that are passed from the OpenGL application to the shaders. This qualifier can be used in both vertex and fragment shaders. For the shaders this is a read-only variable. See Uniform section.
* `varying` – used for interpolated data between a vertex shader and a fragment shader. Available for writing in the vertex shader, and read-only in a fragment shader.

由於 varying 自動雙內插，在 flat 渲染器中必須使用 [OES_standard_derivatives](https://developer.mozilla.org/zh-TW/docs/Web/API/OES_standard_derivatives) 避開自動內插計算。

#### 轉換模型 ####

要把 OpenGL 的模型轉換成 WebGL 用，每一個模型尺度不盡相同，造成直接轉換到螢幕上時發生看不到，無法調整到螢幕正中間，甚至因為物體離鏡頭太遠，物體完全不渲染而無法調整的窘境。常見的處理手法如下：

> 感謝 李佳曄 (Li Jia Ye) 學長熱情協助

1. 計算所有頂點在 x, y, z 三軸上的最大和最小值。
2. 將整個模型頂點平移到原點，直接拿三軸最小值去扣即可。
3. 找出三軸最大和最小值的最大差作為縮放的依準，將物體拉入大小為 1 x 1 x 1 的盒子中。

藉由上述 3 個步驟，我們將所有物體有了統一個格式，方便在 WebGL 載入時有統一的觀看格式，而不用針對每一個模型記錄如何移動到鏡頭前。這裡提供一份簡單的 [C++ 程序](https://github.com/morris821028/hw-computer-graphics/tree/master/tools)。


### 編譯問題 ###

* 測試階段中，容易接收到以下錯誤資訊。
```
Error: WebGL: vertexAttribPointer: -1 is not a valid `index`. 
This value probably comes from a getAttribLocation() call, 
where this return value -1 means that the passed name didn't 
correspond to an active attribute in the specified program.
```
通常是函式 `gl.getAttribLocation(shaderProgram, "aVertexFrontColor")` 抓取參數時，由於程式裡面沒有使用到 `aVertexFrontColor` 變數，僅僅宣告而沒有在實際運算中使用，類似 unused-variable，在其他程式語言中以警告為主，而 WebGL 則以錯誤提示。

* 若在渲染器中遲遲抓不到變數值，有可能是宣告的 buffer 類型不對，如果需要在每一個頂點附加額外的資訊，則提供 `gl.bindBuffer(gl.ARRAY_BUFFER, buffer);`，而不是 `gl.bindBuffer(gl.ELEMENT_ARRAY_BUFFER, buffer);`。


### 參考 ###

* [CSGrandeur 的 WebGL 学习笔记](https://csgrandeur.gitbooks.io/webgl-learn/content/index.html)
