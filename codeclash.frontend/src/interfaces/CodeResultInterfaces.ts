export interface CompileErrorDTO{
    compileError: string;
}

export interface FailedTestErrorDTO{
    failedTestName: string;
    passedTestCount: string;
    allTestsCount: string;
}

export interface ErrorResultDTO{
    compileErrorDTO: CompileErrorDTO;
    failedTestErrorDTO: FailedTestErrorDTO;
}

export interface OkResultDTO{
    meanTime: string;
    passedTestsCount: string;
}

export interface SolutionTestResultDTO{
    okResultDTO: OkResultDTO;
    errorResultDTO: ErrorResultDTO;
}