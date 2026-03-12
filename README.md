# Scalable Memory Game Architecture - Technical Assignment

A dynamic, sprite-driven matching system built in **Unity 2021.3 LTS**. This project demonstrates a clean approach to UI management and state-based game logic, designed to be easily expandable for larger project scopes.

## 🛠 Technical Implementation

### 1. Dynamic Data & Asset Management
* **Sprite-Driven System:** The project utilizes a custom `CardProperties` manager that handles 28 unique fruit assets dynamically.
* **Intelligent Pooling:** The architecture supports any grid size. If the requested number of pairs exceeds the unique asset count, the system automatically reshuffles and repopulates the fruit pool to maintain gameplay variety without errors.

### 2. High-Performance UI Shuffling
* **Sibling Index Manipulation:** To maintain high performance and avoid unnecessary transform calculations, the project utilizes the **Unity UI Sibling Index**. 
* **Layout Agnostic:** By scrambling the hierarchy order, the `GridLayoutGroup` handles the card positioning automatically. This ensures the grid is responsive and compatible with various screen aspect ratios.

### 3. State-Based Selection Logic
* **Validation & Security:** Implemented an integer-based ID tracking system to verify matches. This logic includes checks to prevent "self-matching" bugs (clicking the same object twice).
* **Decoupled Architecture:** Utilizes C# delegates (`OnAnswerCorrect`) to signal successful matches. This keeps the core game logic independent from the UI and visual effects, following professional development standards.

## 🚀 How to Run
1. Clone the repository.
2. Open the project in **Unity 2021.3 LTS**.
3. Open the `Sample Scene` located in `Assets/Scenes`.
4. Press **Play**.

## 📁 Project Structure
* **CardProperties.cs**: The central controller for card data, pooling, and matching logic.
* **CardBehaviour.cs**: Manages individual card states, animations, and input events.
* **CardFaceLoader.cs**: Handles the dynamic slicing and assignment of the 2D fruit sprite sheet.
