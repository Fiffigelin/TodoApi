import { useState } from "react";
import { getTodos, toggleStatus, postTodo, putTodo } from "../todo-query";
import type { Todo, TodoDTO } from "../types";

export const useTodos = () => {
	const [todos, setTodos] = useState<Todo[]>([]);
	const [isLoading, setLoading] = useState<boolean>(false);

	const handleLoadData = async () => {
		setLoading(true);
		try {
			const data = await getTodos();
			setTodos(data);
		} catch (error) {
			console.error(error);
		} finally {
			setLoading(false);
		}
	};

	const handleToggleStatus = async (id: string): Promise<Todo | undefined> => {
		try {
			const data = await toggleStatus(id);

			if (data) {
				setTodos((todos) =>
					todos.map((todo) =>
						todo.id === id ? { ...todo, isComplete: data.isComplete } : todo,
					),
				);
			}

			return data;
		} catch (error) {
			console.error(error);
			return undefined;
		}
	};

	// ej testad
	const createTodo = async (dto: TodoDTO): Promise<Todo | undefined> => {
		try {
			const data = await postTodo(dto);

			if (data) {
				setTodos((prev) => [...prev, data]);
			}

			return data;
		} catch (error) {
			console.error(error);
			return undefined;
		}
	};

	// ej testad
	const updateTodo = async (
		id: string,
		dto: TodoDTO,
	): Promise<Todo | undefined> => {
		try {
			const data = await putTodo(id, dto);

			if (data) {
				setTodos((todos) =>
					todos.map((todo) => (todo.id === id ? data : todo)),
				);
			}

			return data;
		} catch (error) {
			console.error(error);
			return undefined;
		}
	};

	return {
		todos,
		isLoading,
		handleLoadData,
		handleToggleStatus,
		createTodo,
		updateTodo,
	};
};
