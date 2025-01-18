import { CreateAttemptRequest } from "./CreateAttemptRequest";
import { UserAnswer } from "./UserAnswers";

export interface SubmitExamRequestVM{
    Attempt : CreateAttemptRequest;
    UserAnswers : UserAnswer[];
}