using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Components.Web;

namespace BlazorWasmSample.Pages
{
    public partial class Todo
    {
        // Todoアイテム全て
        private List<TodoItem> allTodos = new List<TodoItem>();
        // 追加するアイテム
        private string newItem;
        // 表示用のTodoアイテム
        private List<TodoItem> todosForDisplay = new List<TodoItem>();
        // 現在の表示状態
        private State currentState;
        // 表示状態
        private enum State
        {
            All,
            Active,
            Completed,
        }

        /// <summary>
        /// アイテムを追加
        /// </summary>
        private void AddItem() {
            if (!string.IsNullOrWhiteSpace(newItem))
            {
                allTodos.Add(new TodoItem() { Content = newItem, });
                this.ShowTodo(currentState);
                newItem = string.Empty;
            }
        }

        /// <summary>
        /// Todoアイテム全て表示
        /// </summary>
        private void ShowAll() {
            currentState = State.All;
            this.ShowTodo(currentState);
        }

        /// <summary>
        /// Todo未完了アイテム表示
        /// </summary>
        private void ShowActive() {
            currentState = State.Active;
            this.ShowTodo(currentState);
        }

        /// <summary>
        /// Todo完了アイテム表示
        /// </summary>
        private void ShowCompleted() {
            currentState = State.Completed;
            this.ShowTodo(currentState);
        }

        /// <summary>
        /// Todo完了アイテムをクリア
        /// </summary>
        private void ClearCompleted()
        {
            var completedTodos = allTodos.Where(x => x.IsDone);
            allTodos = allTodos.Except(completedTodos).ToList();
            this.ShowTodo(currentState);
        }

        /// <summary>
        /// ClearCompletedボタン表示スタイルを取得
        /// </summary>
        /// <returns>表示スタイル [inline-block / none]</returns>
        private string GetDisplay()
        {
            return allTodos.Any(x => x.IsDone) ? "inline-block" : "none";
        }

        /// <summary>
        /// Todoアイテムのテキストスタイルを取得
        /// </summary>
        /// <param name="item">Todoアイテム</param>
        /// <returns>文字色と取消線を付けるかどうかのセット</returns>
        private (string foreColor, string decoration) GetTextStyles(TodoItem item)
        {
            return item.IsDone ? ("lightgray", "line-through") : ("black", "none");
        }

        /// <summary>
        /// Todoアイテムを表示
        /// </summary>
        /// <param name="state">状態</param>
        private void ShowTodo(State state)
        {
            switch (state)
            {
                case State.All:
                    todosForDisplay = allTodos;
                    break;
                case State.Active:
                    todosForDisplay = allTodos.Where(x => !x.IsDone).ToList();
                    break;
                case State.Completed:
                    todosForDisplay = allTodos.Where(x => x.IsDone).ToList();
                    break;
            }
        }

        /// <summary>
        /// Enterキーイベント
        /// </summary>
        /// <param name="e">イベントデータ</param>
        private void Enter(KeyboardEventArgs e)
        {
            Console.WriteLine($"KeyboardEventArgs : Code[{e.Code}] Key[{e.Key}]");

            if (e.Key == "Enter")
            {
                this.AddItem();
            }
        }
    }
}