import React from 'react'

type AuthProps = {
    children: React.ReactNode
    fadein?: boolean
}

export default function Auth ({children} : AuthProps) {
    // TODO: re-animate when changes from Register to LogIn
    return <div className="wrapper fadeInDown">
                    {children}
        </div>

}