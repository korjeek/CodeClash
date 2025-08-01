import React from 'react'

type AuthProps = {
    children: React.ReactNode
    fadein?: boolean
}

export default function Auth ({children} : AuthProps) {
    // TODO: re-animate when changes from Register to LogIn
    return <div className="wrapper fadeInDown">
            <link rel="stylesheet" href="./src/style/Register.css"></link>
                    {children}
        </div>

}