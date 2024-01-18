import React from 'react';
import { Helmet } from 'react-helmet-async';

export default function SEO({ image, title, description, name, type }) {
    return (
        <Helmet prioritizeSeoTags>
            { /* Standard metadata tags */}
            <title>{title}</title>
            <meta property="og:image" content={image} />
            <meta property="og:description" content={description} />
            <meta name='description' content={description} />
            { /* End standard metadata tags */}
            { /* Facebook tags */}
            <meta property="og:type" content={type} />
            <meta property="og:title" content={title} />
            {/* <meta property="og:description" content={description} /> */}
            { /* End Facebook tags */}
            { /* Twitter tags */}
            <meta name="twitter:creator" content={name} />
            <meta name="twitter:card" content={type} />
            <meta name="twitter:title" content={title} />
            <meta name="twitter:description" content={description} />
            { /* End Twitter tags */}
        </Helmet>
    )
}