import Card from "@mui/material/Card";
import CardActions from "@mui/material/CardActions";
import CardContent from "@mui/material/CardContent";
import Button from "@mui/material/Button";
import Typography from "@mui/material/Typography";
import type { Todo } from "../api/types";
import { FormControlLabel, FormGroup } from "@mui/material";
import { Checkbox } from "@mui/material";

type CardProps = {
	todo: Todo;
	handleTodoStatus?: () => void;
	handleTodoEdit?: () => void;
	handleToggleStatus: (id: string) => Promise<Todo | undefined>;
};

export default function TodoCard({ ...props }: CardProps) {
	return (
		<Card sx={{ minWidth: 350 }} className="flex flex-col justify-between">
			<CardContent>
				<Typography
					gutterBottom
					sx={{ color: "text.secondary", fontSize: 14 }}
					className="uppercase"
				>
					{props.todo.name}
				</Typography>
				<Typography variant="body2">{props.todo.description}</Typography>
				<FormGroup>
					<FormControlLabel
						control={
							<Checkbox
								checked={props.todo.isComplete}
								onChange={() => props.handleToggleStatus(props.todo.id)}
							/>
						}
						label="Är gjord"
					/>
				</FormGroup>
			</CardContent>
			<CardActions className="flex justify-evenly">
				<Button size="small">Redigera</Button>
				<Button size="small">Radera</Button>
			</CardActions>
		</Card>
	);
}

// (Bruteforce, realattacker, phising, sqlinjections, rainbowattacks);
