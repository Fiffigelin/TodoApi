export type Todo = {
	id: string;
	name: string;
	description?: string;
	isComplete: boolean;
};

export type TodoDTO = {
	name: string;
	description?: string;
};
