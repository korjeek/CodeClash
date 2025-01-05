import React from 'react';
import AuthNavBar from "../NavBars/AuthNavBar.tsx";
import '../../style/Default/BackGround.css'
import '../../style/AuthPage/Main.css'
import '../../style/AuthPage/AuthField.css'
import '../../style/AuthPage/Input.css'

interface AuthPageProps {
    AuthPageElement: React.ReactNode;
}

const AuthPage: React.FC<AuthPageProps> = ({ AuthPageElement }) => {
    return(
        <div className="register-page">
            <div className="ellipses-container">
                <div className="ellipse bottom-ellipse"/>
                <div className="ellipse inner-ellipse"/>
                <div className="first-fade-ellipse"/>
                <div className="second-fade-ellipse"/>
                <div className="third-fade-ellipse"/>
                <div className="ellipse right-ellipse"/>
                <div className="ellipse top-ellipse"/>
            </div>
            <div className="main-container">
                <div className="glass-effect"/>
                <AuthNavBar/>
                <main className="auth-main-wrapper">
                    <div className="auth-container">
                        <div className="auth-section">
                            {AuthPageElement}
                        </div>
                    </div>
                </main>
            </div>
        </div>
    )
};

export default AuthPage;