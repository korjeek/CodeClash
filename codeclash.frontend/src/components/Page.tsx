import Header from "./Header";
import TopNavBar from "./TopNavBar";

interface PageProp {
    navBarIndex?: number;
    children?: React.ReactNode;
}

export default function Page ({navBarIndex, children} : PageProp) {
    if (navBarIndex == undefined) {navBarIndex = -1;}
    return (
        <div>
            <header><Header/></header>
            <section id="mainMenu">
                <TopNavBar index={navBarIndex} />
            </section>
            <div id="mainContentHolder">
                {children}
            </div>
            <footer>Powered by <b>BetBoom</b></footer>
        </div>
    );
}