export interface AddQuizzesRequest{
    title: string;
    description: string;
    config?: any;
    createdAt?: Date;
    lastUpdateAt?: Date;
    userId?: number;
    questions: Question[];
    subject:string;
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
  