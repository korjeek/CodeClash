import {useState} from 'react'

type allowedFileExtensions = ".cs" | ".py" | ".cpp" | ".java" | ".html, .xml"

export function CodePallete() {
    const [fileExtensions, setFileExtensions] = useState<allowedFileExtensions>(".cs");
    const allowedExtensions: allowedFileExtensions[] = [".cs", ".cpp", ".cpp", ".java", ".html, .xml"]
    return (
        <form onSubmit={(event) => {console.log(event.currentTarget)}}>
            <table>
                <tbody style={{alignContent: "center"}}>
                    <tr>
                        <td style={{maxWidth: "30vw"}}><h3>Choose programming language:</h3></td>
                        <td style={{minWidth: "70vw", alignContent: "right"}}>
                            <select name="Choose Language" onChange={(event) => {
                                    console.log(event.currentTarget.selectedIndex)
                                    setFileExtensions(allowedExtensions[event.currentTarget.selectedIndex])
                                }} defaultValue={0}>
                                <option value="csharp">C#</option>
                                <option value="cpp">C++</option>
                                <option value="python">Python</option>
                                <option value="java">Java</option>
                                <option value="html">HTML</option>
                            </select>
                        </td>
                    </tr>
                    <tr>
                        <td><h3>Enter your code</h3></td>
                        <td><textarea name="inputField" style={{maxHeight: "30vh", minHeight: "30vh", minWidth: "60vw", maxWidth: "65vw"}}/></td>
                    </tr>
                    <tr>
                        <td><h3>Or choose file</h3></td>
                        <td><input type="file" accept={fileExtensions} onChange={(event) => {
                            console.log(event.currentTarget);
                        }}/></td>
                    </tr>
                    <tr>
                        <td><button onClick={(event) => {console.log(event.currentTarget)}}>Run</button></td>
                        <td><input type="submit"/></td>
                    </tr>
                </tbody>
            </table>
        </form>
    )
}