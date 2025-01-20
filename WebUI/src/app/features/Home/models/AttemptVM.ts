import { Quizz } from '../../report/models/report';

export interface AttemptVM {
  score: number;
  attemptAt: Date | null;
  name: string;
  duration: number;
  quizzes: Quizz;
}
