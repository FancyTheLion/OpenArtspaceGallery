const apiBaseUrl = import.meta.env.VITE_BACKEND_URL

// Make GET request
async function WebClientSendGetRequest
(
    relativeUrl: string
)
{
    const response = await fetch(apiBaseUrl + relativeUrl, {
        method: 'GET'
    })

    return response;
}

// Make POST request
async function WebClientSendPostRequest
(
    relativeUrl: string,
    request: object
)
{
    const response = await fetch(apiBaseUrl + relativeUrl, {
        method: 'POST',
        body: JSON.stringify(request),
        headers: { "Content-Type": "application/json" }
    })

    return response
}

async function WebClientSendDeleteRequest
(
    relativeUrl: string
)
{
    const response = await fetch(apiBaseUrl + relativeUrl, {
        method: 'DELETE'
    })

    return response
}

    async function WebClientPostForm(
        relativeUrl: string,
        request: FormData
    ) {
        const formData = request instanceof FormData;

        const response = await fetch(apiBaseUrl + relativeUrl, {
            method: 'POST',
            body: request,
            headers: formData ? undefined : { 'Content-Type': 'application/json' }
        });

        return response;
    }

export
{
    WebClientSendGetRequest,
    WebClientSendPostRequest,
    WebClientSendDeleteRequest,
    WebClientPostForm
}
