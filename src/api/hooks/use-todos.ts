import { getTodos } from "../todo-query";

export const useTodos = async () => {
	const todos = await getTodos();

	console.log(todos);
	return todos;
};
