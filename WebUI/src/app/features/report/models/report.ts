export interface Quizz {
    id:string,
    title:string,
    description:string,
    config:string,
    status:boolean,
    createdAt:Date,
    lastUpdateAt:Date
}

export interface Question{
    id:string,
    questionContent:string,
    type:string,
    createdAt:Date,
    updatedAt:Date
}

export interface Rank{
    userName:string,
    image:string,
    score:number,
    attemptAt:Date,
    duration:number
}
