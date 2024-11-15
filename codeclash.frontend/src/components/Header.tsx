import {Link} from 'react-router-dom'

export default function Header () {
    return (
        <div id="header">
            <h1 className='changeColors'>CodeClash</h1>
            <a href='/login' style={{"float": "right"}}>Log In</a>
        </div>
    );
}