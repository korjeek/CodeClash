export default function TopNavBar ({index} : {index: number}) {

    return (
        <div className="topnav" id="mainMenu">
            <a href="/home" className={index == 0 ? "active" : "inactive"}>Home</a>
            <a href="/rooms" className={index == 1 ? "active" : "inactive"}>Rooms</a>
            <a href="/ranks" className={index == 2 ? "active" : "inactive"}>Ranks</a>
        </div>
    );
}