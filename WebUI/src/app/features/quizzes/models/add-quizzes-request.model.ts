export interface AddQuizzesRequest {
  title: string;
  description: string;
  config?: any;
  createdAt?: Date;
  lastUpdateAt?: Date;
  userId?: number;
  status: boolean;
  questions: Question[];
}

export interface Question {
  questionContent: string;
  questionType: string;
  answers: Answer[];
}

export interface Answer {
  answerContent: string;
  isCorrect: boolean;
}
