import { Answer } from "./Answer"

export interface Question{
    id: string,
    questionContent: string,
    questionType: number,
    createdAt: Date,
    updatedAt: Date,
    quizzId: string
    answers : Answer[]
}