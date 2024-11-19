export default function Header () {
    return (
        <div id="header">
            <h1 className='changeColors' style={{"padding-bottom": "15px", "top": "0px"}}>CodeClash</h1>
            <a href='/login' style={{"float": "right", "padding-right" : "10px"}}>Log In</a>
        </div>
    );
}