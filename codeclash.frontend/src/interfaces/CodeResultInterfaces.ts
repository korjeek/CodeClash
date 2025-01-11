export interface CompileErrorDTO{
    compileError: string;
}

export interface FailedTestErrorDTO{
    failedTestName: string;
    passedTestCount: string;
    allTestsCount: string;
}

export interface ErrorResultDTO{
    compileError: CompileErrorDTO;
    failedTestError: FailedTestErrorDTO;
}

export interface OkResultDTO{
    meanTime: string;
    passedTestsCount: string;
}

export interface SolutionTestResultDTO{
    okResult: OkResultDTO;
    errorResult: ErrorResultDTO;
}