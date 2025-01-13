export interface Quizz {
    id:string,
    title:string,
    description:string,
    config:string,
    status:boolean,
    createAt:Date,
    lastUpdateAt:Date
}

export interface Question{
    id:string,
    questionContent:string,
    type:string,
    createAt:Date,
    updateAt:Date
}

export interface Rank{
    userName:string,
    image:string,
    score:number,
    attemptAt:Date,
    duration:number
}
