import { useEffect } from "react";
import "./App.css";
import { useTodos } from "./api/hooks/use-todos";
import TodoCard from "./components/todo-card";
// import type { TodoDTO } from "./api/types";

// const INIT_DTO: TodoDTO = {
// 	name: "",
// 	description: "",
// };

function App() {
	const { todos, isLoading, handleLoadData, handleToggleStatus } = useTodos();
	// const [dto, setDto] = useState<TodoDTO>(INIT_DTO);

	useEffect(() => {
		handleLoadData();
	}, []);

	return (
		<div className="w-full p-8 flex justify-center">
			<div className="flex flex-col gap-6">
				{isLoading ? (
					<p>Laddar</p>
				) : (
					todos.map((t) => (
						<TodoCard
							key={t.id}
							todo={t}
							handleToggleStatus={handleToggleStatus}
						/>
					))
				)}
			</div>
		</div>
	);
}

export default App;
