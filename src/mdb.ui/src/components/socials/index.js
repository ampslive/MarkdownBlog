import React, { useState, useEffect } from 'react'
import { Link } from 'react-router-dom';

function Socials(props) {

    const providers = props.provider;
    const [socials, setSocials] = useState([{ icon : "", uri : "" }]);

    const socialMediaProvider = [
        "bi bi-twitter", //twitter
        "bi bi-linkedin", //linkedin
        "bi bi-youtube", //youtube
        "bi bi-threads-fill", //threads
        "bi bi-mastodon", //mastodon
        "bi bi-facebook", //facebook
        "bi bi-envelope-at-fill", //mail
        "bi bi-instagram"  //instagram
    ];

    useEffect(() => {

        var socialData = providers?.map(i => ({
            icon : socialMediaProvider[i.Provider],
            uri : (i.Provider === 6) ? "mailto:" + i.Uri : i.Uri
        }));

        setSocials(socialData);
    }, [providers]);


    return (
        <>
        {socials && (
            <ul class="list-group list-group-horizontal">
                {socials.map(i => (
                    <li key={i.uri} class="list-group-item">
                    <Link to={i.uri} target="_blank" class="nav-link">
                        <i className={i.icon}></i>
                    </Link>
                </li>
                ))}
            </ul>
        )}
        </>
    );

}

export default Socials;