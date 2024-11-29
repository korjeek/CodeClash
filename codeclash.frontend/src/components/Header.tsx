export default function Header () {
    return (
        <div id="header">
            <h1 className='changeColors' style={{paddingBottom: "15px", top: "0px"}}>CodeClash</h1>
            <a href='/login' style={{float: "right", paddingRight : "10px"}}>Log In</a>
        </div>
    );
}