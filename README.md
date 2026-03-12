# 2D Fruit Matching Game - Technical Assignment

A scalable matching game built in **Unity 2021.3 LTS**. This project demonstrates clean C# architecture, dynamic UI management, and efficient state handling tailored for mobile or web performance.

## 🛠 Technical Implementation

### 1. Data Management & Scalability
* **Dynamic Sprite Assignment:** Uses a custom loader to handle the 28 unique fruit sprites from the provided sprite sheet.
* **Smart Pool Logic:** Designed to handle any grid size. If the grid requires more than 28 pairs, the system automatically reshuffles the fruit pool to prevent duplication patterns or index errors.

### 2. UI Shuffling & Layout
* **Sibling Index Manipulation:** Utilized the Unity UI Sibling Index to scramble card positions. This allows the **GridLayoutGroup** to handle the heavy lifting of positioning while maintaining high performance.
* **Fisher-Yates Algorithm:** Ensures a mathematically "fair" shuffle every time the game starts.

### 3. Selection & Match Logic
* **Integer-Based State Tracking:** Optimized memory usage by tracking selection via integer IDs.
* **Input Validation:** Prevents common bugs such as "same-card matching" (clicking the same object twice).
* **Event-Driven Design:** Employs delegates (`OnAnswerCorrect`) to keep the UI and Game Logic decoupled.

## 🚀 How to Run
1. Clone the repository.
2. Open the project in **Unity 2021.3 LTS**.
3. Open `SampleScene` from the `Assets/Scenes` folder.
4. Press **Play**.
