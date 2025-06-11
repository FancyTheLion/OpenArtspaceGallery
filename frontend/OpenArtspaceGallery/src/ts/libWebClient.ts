const apiBaseUrl = import.meta.env.VITE_BACKEND_URL

// Make GET request
async function WebClientSendGetRequest
(
    relativeUrl: string
)
{
    return  await fetch(apiBaseUrl + relativeUrl, {
        method: 'GET'
    })
}

// Make POST request
async function WebClientSendPostRequest
(
    relativeUrl: string,
    request: object
)
{
    return  await fetch(apiBaseUrl + relativeUrl, {
        method: 'POST',
        body: JSON.stringify(request),
        headers: { "Content-Type": "application/json" }
    })
}

async function WebClientSendDeleteRequest
(
    relativeUrl: string
)
{
    return  await fetch(apiBaseUrl + relativeUrl, {
        method: 'DELETE'
    })
}

    async function WebClientPostForm(
        relativeUrl: string,
        request: FormData
    ) {
        const formData = request instanceof FormData;

        return  await fetch(apiBaseUrl + relativeUrl, {
            method: 'POST',
            body: request,
            headers: formData ? undefined : { 'Content-Type': 'application/json' }
        });
    }

export
{
    WebClientSendGetRequest,
    WebClientSendPostRequest,
    WebClientSendDeleteRequest,
    WebClientPostForm
}
