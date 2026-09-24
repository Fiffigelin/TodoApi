import { useState } from "react";
import { getTodos, toggleStatus } from "../todo-query";
import type { Todo } from "../types";

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

	return {
		todos,
		isLoading,
		handleLoadData,
		handleToggleStatus,
	};
};
