import '../../style/StartPage/Main.css'
import '../../style/Default/BackGround.css'
import AuthNavBar from "../NavBars/AuthNavBar.tsx";
import {useNavigate} from "react-router-dom";


export default function StartPage() {
    const navigate = useNavigate();

    const getStartedClick = async () => navigate('/login');

    return (
        <div className="home-page">
            <AuthNavBar/>
            <main className="home-main-wrapper">
                <header className="section_header position-relative">
                    <div className="text-align-center">
                        <h1 className="hero-section">
                            <span className="text-style-hero">CodeClash</span>
                            <br/>challenge the strongest
                        </h1>
                        <button className="get-started-button" onClick={getStartedClick}>Get Started</button>
                    </div>
                </header>
            </main>
            <section>
                <div className="first-ellipse-container">
                    <div className="ellipse bottom-ellipse"/>
                    <div className="ellipse inner-ellipse"/>
                </div>
                <div className="first-fade-ellipse"/>
                <div className="second-fade-ellipse"/>
                <div className="third-fade-ellipse"/>
                <div className="ellipse right-ellipse"/>
                <div className="ellipse top-ellipse"/>
            </section>
        </div>
    )
};




