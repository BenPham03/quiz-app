export interface AddQuizzesRequest{
    title: string;
    description: string;
    config: string;
    subject: string;
    questions: Question[];
}

interface Question {
    questionText: string;
    questionType: string;
    answers: Answer[];
    correctAnswerIndex: number; 
  }
  interface Answer {
    answerText: string;
  }